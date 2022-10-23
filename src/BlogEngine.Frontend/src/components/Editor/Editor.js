import { useState, useEffect } from "react";
import "react-quill/dist/quill.snow.css";
import { Theme, presetGpnDefault } from "@consta/uikit/Theme";
import { Text } from "@consta/uikit/Text";
import { Button } from "@consta/uikit/Button";
import { Steps } from "@consta/uikit/Steps";
import { readBase64 } from "../../lib/Helpers";
import { Layout } from "@consta/uikit/Layout";
import { Issue, Article, Auth } from "../../lib/ApiClient";
import { useNavigate } from "react-router-dom";
import { useForceUpdate } from "../../hooks/useForceUpdate";
import { renderTags, renderSetup, renderEditor } from "./helpRender";

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

export default function Editor(props) {
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
        {props.Header}
      </Text>
      <Steps
        size="l"
        items={items}
        getItemLabel={(item) => item.label}
        value={currStep}
        onChange={(item) => setCurrStep(item)}
      />
      {activeStepIndex === 2 &&
        renderTags(tag, tags, update, setTag, setTags, sendArticle)}
      {activeStepIndex === 1 && renderEditor(value, modules, formats, setValue)}
      {activeStepIndex === 0 &&
        renderSetup(
          title,
          setTitle,
          issueSelect,
          issue,
          setIssue,
          setDesc,
          setFiles,
          files,
          desc
        )}
      <div>&nbsp;</div>
      <Layout style={{ justifyContent: "space-evenly" }}>
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
