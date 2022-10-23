import { Modal } from "@consta/uikit/Modal";
import { useState } from "react";
import { TextField } from "@consta/uikit/TextField";
import { Layout } from "@consta/uikit/Layout";
import { Auth } from "../../../lib/ApiClient";
import { Text } from "@consta/uikit/Text";
import { Button } from "@consta/uikit/__internal__/src/components/Button/Button";
import { DragNDropField } from "@consta/uikit/DragNDropField";
import { Attachment } from "@consta/uikit/Attachment";
import { readBase64 } from "../../../lib/Helpers";

export function RegisterModal(props) {
  const [login, setLogin] = useState(null);
  const [password, setPassword] = useState(null);
  const [firstName, setFirstName] = useState(null);
  const [lastName, setLastName] = useState(null);
  const [files, setFiles] = useState([]);
  const [image, setImage] = useState(null);

  files[0] && readBase64(files[0], (a) => setImage(a));
  return (
    <Modal
      style={{ zIndex: 999 }}
      isOpen={props.isOpen}
      hasOverlay
      onClickOutside={props.onClickOutside}
      onEsc={props.onEsc}
    >
      <div style={{ margin: 30 }}>
        <Layout direction="column">
          <Text
            align="center"
            size="xl"
            weight="bold"
            style={{ marginBottom: 20 }}
          >
            Регистрация
          </Text>
          <Layout>
            <TextField
              onChange={({ value }) => setFirstName(value)}
              value={firstName}
              type="text"
              placeholder="Введите имя"
              label="Имя"
              indent="l"
              width="full"
            />
          </Layout>
          <div>&nbsp;</div>
          <Layout>
            <TextField
              onChange={({ value }) => setLastName(value)}
              value={lastName}
              type="text"
              placeholder="Введите фамилию"
              label="Фамилия"
              indent="l"
              width="full"
            />
          </Layout>
          <div>&nbsp;</div>
          <Layout>
            <TextField
              onChange={({ value }) => setLogin(value)}
              value={login}
              type="text"
              placeholder="Введите логин"
              label="Логин"
              indent="l"
              width="full"
            />
          </Layout>
          <div>&nbsp;</div>
          <Layout>
            <TextField
              onChange={({ value }) => setPassword(value)}
              value={password}
              type="password"
              placeholder="Введите пароль"
              label="Пароль"
              indent="l"
              width="full"
            />
          </Layout>
          <div>&nbsp;</div>
          <Layout>
            <DragNDropField multiple={false} onDropFiles={setFiles}>
              <Text>Перетяните файл сюда</Text>
            </DragNDropField>
          </Layout>
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
          <div>&nbsp;</div>
          <Button
            size="m"
            view="primary"
            form="round"
            label="Зарегистрироваться"
            onClick={() => {
              var item = {
                nickname: login,
                password: password,
                firstName: firstName,
                lastName: lastName,
                image: image,
              };
              Auth.register(item);
              props.onClickOutside();
            }}
          />
        </Layout>
      </div>
    </Modal>
  );
}
