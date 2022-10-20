import { Theme, presetGpnDefault } from "@consta/uikit/Theme";
import { Text } from "@consta/uikit/Text";
import { Header, HeaderLogo, HeaderModule } from "@consta/uikit/Header";
import { Button } from "@consta/uikit/__internal__/src/components/Button/Button";
import { IconUser } from "@consta/uikit/IconUser";
import { useState } from "react";
import { LoginModal } from "../Modals/LoginModal/LoginModal";

export default function EngineHeader() {
  const [isLoginModalOpen, setIsLoginModalOpen] = useState(false);
  return (
    <Theme className="App" preset={presetGpnDefault}>
      <Header
        leftSide={
          <>
            <HeaderModule>
              <HeaderLogo>
                <Text as="p" size="l" weight="bold">
                  Logotype
                </Text>
              </HeaderLogo>
            </HeaderModule>
          </>
        }
        rightSide={
          <>
            <HeaderModule indent="s">
              <Button
                size="m"
                view="secondary"
                form="round"
                label="Войти"
                iconLeft={IconUser}
                onClick={() => setIsLoginModalOpen(true)}
              />
            </HeaderModule>
            <HeaderModule indent="l">
              <Button
                size="m"
                view="primary"
                form="round"
                label="Зарегистрироваться"
                iconLeft={IconUser}
              />
            </HeaderModule>
          </>
        }
      />
      <LoginModal
        isOpen={isLoginModalOpen}
        onClickOutside={() => setIsLoginModalOpen(false)}
        onEsc={() => setIsLoginModalOpen(false)}
      />
    </Theme>
  );
}
