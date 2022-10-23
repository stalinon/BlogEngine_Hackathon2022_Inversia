import { Modal } from "@consta/uikit/Modal";
import { useState } from "react";
import { TextField } from "@consta/uikit/TextField";
import { Layout } from "@consta/uikit/Layout";
import { Auth } from "../../../lib/ApiClient";
import { Text } from "@consta/uikit/Text";
import { Button } from "@consta/uikit/__internal__/src/components/Button/Button";

export function LoginModal(props) {
  const [login, setLogin] = useState(null);
  const [password, setPassword] = useState(null);

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
            Вход
          </Text>
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
          <div>&nbsp;</div>
          <Button
            size="m"
            view="primary"
            form="round"
            label="Войти"
            onClick={() => {
              var item = { nickname: login, password: password };
              Auth.login(item)
                .then((res) => props.onClickOutside())
                .then(() => window.location.reload());
            }}
          />
        </Layout>
      </div>
    </Modal>
  );
}
