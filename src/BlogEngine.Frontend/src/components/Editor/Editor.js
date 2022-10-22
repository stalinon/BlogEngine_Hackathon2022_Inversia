import ReactQuill from "react-quill";
import { useState, useEffect } from "react";
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
import { Issue, Article, Auth } from "../../lib/ApiClient";
import { useNavigate } from "react-router-dom";
import { Select } from "@consta/uikit/Select";
import { Tag } from "@consta/uikit/Tag";
import { useForceUpdate } from "../../hooks/useForceUpdate";

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
  { id: 3, label: "Добавление тегов" },
];

export default function Editor() {
  const [value, setValue] = useState(null);
  const [desc, setDesc] = useState(null);
  const [title, setTitle] = useState(null);
  const [files, setFiles] = useState([]);
  const [image, setImage] = useState(null);
  const [issue, setIssue] = useState(null);
  const [issueSelect, setIssueSelect] = useState([]);
  const [currStep, setCurrStep] = useState(items[0]);
  const activeStepIndex = items.findIndex((item) => item === currStep);
  const navigate = useNavigate();
  const update = useForceUpdate();

  const [tag, setTag] = useState(null);
  const [tags, setTags] = useState([]);

  const handleNext = () => setCurrStep(items[activeStepIndex + 1]);
  const handlePrev = () => setCurrStep(items[activeStepIndex - 1]);

  files[0] && readBase64(files[0], (a) => setImage(a));

  useEffect(() => {
    !issueSelect[0] &&
      Issue.get().then((res) => {
        setIssueSelect(
          res.data.map((x, i) => {
            return {
              id: i,
              issue_id: x.id,
              label: `ИНверсия, выпуск ${x.issue_number} от ${new Date(
                x.date
              ).toLocaleString("ru", {
                year: "numeric",
                month: "numeric",
                day: "numeric",
                timezone: "UTC",
              })}`,
            };
          })
        );
      });
  }, [issueSelect]);

  function sendArticle() {
    Auth.me().then((res) => {
      let article = {
        header: title,
        desc: desc,
        leading_image: image,
        author: res.data,
        text: value,
        issue_id: issue.issue_id,
        tags: tags,
      };
      console.log(issue);
      console.log(article);
      Article.post(article).then((res) => navigate("/"));
    });
  }

  function renderTags() {
    return (
      <div>
        <Text
          as="h2"
          cursor="pointer"
          display="inline-block"
          size="l"
          weight="bold"
        >
          Добавьте теги для статьи
        </Text>
        <div>&nbsp;</div>
        <Layout direction="row">
          <Layout>
            <TextField
              label="Введите тег и нажмите на кнопку, чтобы добавить"
              onChange={({ value }) => setTag(value)}
              value={tag}
            />
          </Layout>
          <Layout
            style={{
              margin: "28pt",
            }}
          >
            <Button
              label="+"
              size="s"
              view="secondary"
              onClick={() => {
                let tmp = tags;
                tmp.push(tag);
                setTags(tmp);
                setTag(null);
              }}
            />
          </Layout>
        </Layout>

        {tags.map((t, i) => {
          return (
            <Tag
              key={i}
              group={i}
              mode="cancel"
              label={t}
              style={{ margin: 5 }}
              onCancel={() => {
                let tmp = tags;
                tmp.splice(i, 1);
                setTags(tmp);
                update();
              }}
            />
          );
        })}

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
        <Select
          items={issueSelect}
          placeholder="Выберите выпуск"
          label="Выберите выпуск журнала"
          onChange={({ value }) => setIssue(issueSelect[value.id])}
          value={issue}
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
        <Button
          size="l"
          label="Опубликовать"
          width="full"
          onClick={sendArticle}
        />
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
      {activeStepIndex === 2 && renderTags()}
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
