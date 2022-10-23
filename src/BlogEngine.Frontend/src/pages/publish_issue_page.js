import { Theme, presetGpnDefault } from "@consta/uikit/Theme";
import { Button } from "@consta/uikit/Button";
import { TextField } from "@consta/uikit/TextField";
import { DragNDropField } from "@consta/uikit/DragNDropField";
import { Layout } from "@consta/uikit/Layout";
import { useState } from "react";
import { DatePicker } from "@consta/uikit/DatePicker";
import { useNavigate } from "react-router-dom";
import { Text } from "@consta/uikit/Text";
import { Attachment } from "@consta/uikit/Attachment";
import { Issue } from "../lib/ApiClient";
import { readBase64 } from "../lib/Helpers";

export default function PublishIssuePage() {
  const [date, setDate] = useState(null);
  const [number, setNumber] = useState(null);
  const [files, setFiles] = useState([]);
  const [pdfs, setPdfs] = useState([]);
  const [image, setImage] = useState(null);
  const [pdf, setPdf] = useState(null);
  const navigate = useNavigate();

  files[0] && readBase64(files[0], (a) => setImage(a));
  pdfs[0] && readBase64(pdfs[0], (a) => setPdf(a));
  return (
    <Theme className="App" preset={presetGpnDefault}>
      <div
        className="publish-issue-page"
        style={{ margin: 100, height: "100%" }}
      >
        <Layout direction="column">
          <Text
            as="h1"
            align="center"
            cursor="pointer"
            display="inline-block"
            size="xxl"
            weight="bold"
            style={{ marginBottom: 20 }}
          >
            Создайте новый выпуск
          </Text>
          <Layout>
            <TextField
              onChange={({ value }) => setNumber(value)}
              value={number}
              type="number"
              placeholder="Введите номер выпуска"
              label="Номер выпуска"
              indent="l"
              width="full"
            />
          </Layout>
          <div>&nbsp;</div>
          <Layout>
            <DatePicker
              onChange={({ value }) => setDate(value)}
              value={date}
              label="Дата выпуска"
              indent="l"
              width="full"
            />
          </Layout>
          <div>&nbsp;</div>
          <DragNDropField
            accept="image/*"
            multiple={false}
            onDropFiles={setFiles}
          >
            <Text>Перетяните фото обложки сюда</Text>
          </DragNDropField>
          <div>&nbsp;</div>
          <Layout>
            {files.map((file) => {
              return (
                <Attachment
                  key={file.name}
                  fileName={file.name}
                  fileExtension={file.name.match(/\.(?!.*\.)(\w*)/)?.[1]}
                  fileDescription={file.type}
                />
              );
            })}
          </Layout>
          <div>&nbsp;</div>
          <DragNDropField accept=".pdf" multiple={false} onDropFiles={setPdfs}>
            <Text>Перетяните .pdf-файл сюда</Text>
          </DragNDropField>
          <div>&nbsp;</div>
          <Layout>
            {pdfs.map((file) => {
              return (
                <Attachment
                  key={file.name}
                  fileName={file.name}
                  fileExtension={file.name.match(/\.(?!.*\.)(\w*)/)?.[1]}
                  fileDescription={file.type}
                />
              );
            })}
          </Layout>
          <div>&nbsp;</div>
          <div>&nbsp;</div>
          <Button
            size="m"
            view="primary"
            label="Создать выпуск"
            onClick={() => {
              var item = {
                date: date,
                issue_number: number,
                leading_image: image,
                pdf: pdf,
              };
              Issue.post(item).then(() => navigate("/issues"));
            }}
          />
        </Layout>
      </div>
    </Theme>
  );
}
