import ReactQuill from "react-quill";
import { useState } from "react";
import { TextField } from "@consta/uikit/TextField";
import "react-quill/dist/quill.snow.css";
import { Theme, presetGpnDefault } from "@consta/uikit/Theme";
import { DragNDropField } from "@consta/uikit/DragNDropField";
import { Text } from "@consta/uikit/Text";
import { Attachment } from "@consta/uikit/Attachment";
import { Button } from "@consta/uikit/Button";
import { Steps } from "@consta/uikit/Steps";
import { readBase64 } from "../../lib/Helpers";
import { Layout } from "@consta/uikit/Layout";
import { Article, Auth } from "../../lib/ApiClient";
import { useNavigate } from "react-router-dom";

const modules = {
  toolbar: [
    [{ header: "1" }, { header: "2" }, { font: [] }],
    [{ size: [] }],
    ["bold", "italic", "underline", "strike", "blockquote"],
    [
      { list: "ordered" },
      { list: "bullet" },
      { indent: "-1" },
      { indent: "+1" },
    ],
    ["link", "image", "video"],
  ],
  clipboard: {
    matchVisual: false,
  },
};

const formats = [
  "header",
  "font",
  "size",
  "bold",
  "italic",
  "underline",
  "strike",
  "blockquote",
  "list",
  "bullet",
  "indent",
  "link",
  "image",
  "video",
];

const items = [
  { id: 1, label: "Настройка статьи" },
  { id: 2, label: "Написание текста" },
];

export default function Editor() {
  const [value, setValue] = useState(null);
  const [desc, setDesc] = useState(null);
  const [title, setTitle] = useState(null);
  const [files, setFiles] = useState([]);
  const [image, setImage] = useState(null);
  const [currStep, setCurrStep] = useState(items[0]);
  const activeStepIndex = items.findIndex((item) => item === currStep);
  const navigate = useNavigate();

  const handleNext = () => setCurrStep(items[activeStepIndex + 1]);
  const handlePrev = () => setCurrStep(items[activeStepIndex - 1]);

  files[0] && readBase64(files[0], (a) => setImage(a));

  function sendArticle() {
    Auth.me().then((res) => {
      let article = {
        header: title,
        desc: desc,
        leading_image: image,
        author: res.data,
        text: value,
      };
      Article.post(article).then((res) => navigate("/"));
    });
  }

  function renderSetup() {
    return (
      <div>
        <Text
          as="h2"
          cursor="pointer"
          display="inline-block"
          size="l"
          weight="bold"
        >
          Настройте статью
        </Text>
        <div>&nbsp;</div>
        <TextField
          value={title}
          onChange={({ value }) => setTitle(value)}
          type="text"
          label="Введите заголовок для статьи"
          placeholder="Заголовок"
          view="default"
          form="default"
          width="full"
          size="l"
          requered="true"
        />
        <div>&nbsp;</div>
        <TextField
          value={desc}
          onChange={({ value }) => setDesc(value)}
          type="textarea"
          label="Введите описание статьи"
          placeholder="Описание"
          view="default"
          form="default"
          width="full"
          size="l"
          requered="true"
          minRows="3"
        />
        <div>&nbsp;</div>
        <DragNDropField
          multiple={false}
          accept="image/*"
          onDropFiles={setFiles}
        >
          <Text>Загрузите ведущее фото для статьи</Text>
        </DragNDropField>
        {files.map((file) => (
          <Attachment
            key={file.name}
            fileName={file.name}
            fileExtension={file.name.match(/\.(?!.*\.)(\w*)/)?.[1]}
            fileDescription={file.type}
          />
        ))}
        <div>&nbsp;</div>
      </div>
    );
  }

  function renderEditor() {
    return (
      <div>
        <Text
          as="h2"
          cursor="pointer"
          display="inline-block"
          size="l"
          weight="bold"
        >
          Введите текст статьи в редакторе ниже
        </Text>
        <div>&nbsp;</div>
        <ReactQuill
          theme="snow"
          value={value}
          onChange={setValue}
          modules={modules}
          formats={formats}
          className="editor"
        />
        <div>&nbsp;</div>
        <Button
          size="l"
          label="Опубликовать"
          width="full"
          onClick={sendArticle}
        />
      </div>
    );
  }

  return (
    <Theme
      className="App"
      preset={presetGpnDefault}
      style={{ margin: 20, height: "100%" }}
    >
      <Text
        as="h1"
        align="center"
        cursor="pointer"
        display="inline-block"
        size="xxl"
        weight="bold"
      >
        Публикация новой статьи
      </Text>
      <Steps
        size="l"
        items={items}
        getItemLabel={(item) => item.label}
        value={currStep}
        onChange={(item) => setCurrStep(item)}
      />
      {activeStepIndex === 1 && renderEditor()}
      {activeStepIndex === 0 && renderSetup()}
      <div>&nbsp;</div>
      <Layout>
        <Button
          label="Предыдущий"
          onClick={handlePrev}
          disabled={activeStepIndex === 0}
        />
        <Button
          label="Следующий"
          onClick={handleNext}
          disabled={activeStepIndex === items.length - 1}
        />
      </Layout>
    </Theme>
  );
}
