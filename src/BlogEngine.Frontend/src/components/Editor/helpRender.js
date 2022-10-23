import ReactQuill from "react-quill";
import { TextField } from "@consta/uikit/TextField";
import "react-quill/dist/quill.snow.css";
import { DragNDropField } from "@consta/uikit/DragNDropField";
import { Text } from "@consta/uikit/Text";
import { Attachment } from "@consta/uikit/Attachment";
import { Button } from "@consta/uikit/Button";
import { Layout } from "@consta/uikit/Layout";
import { Select } from "@consta/uikit/Select";
import { Tag } from "@consta/uikit/Tag";

export function renderTags(tag, tags, update, setTag, setTags, sendArticle) {
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

export function renderSetup(
  title,
  setTitle,
  issueSelect,
  issue,
  setIssue,
  setDesc,
  setFiles,
  files,
  desc
) {
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
      <DragNDropField multiple={false} accept="image/*" onDropFiles={setFiles}>
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

export function renderEditor(value, modules, formats, setValue) {
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
