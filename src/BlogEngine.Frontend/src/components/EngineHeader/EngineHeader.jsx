import { Theme, presetGpnDefault, presetGpnDark } from "@consta/uikit/Theme";
import {
  Header,
  HeaderLogo,
  HeaderModule,
  HeaderMenu,
} from "@consta/uikit/Header";
import { Button } from "@consta/uikit/__internal__/src/components/Button/Button";
import { IconUser } from "@consta/uikit/IconUser";
import { IconAdd } from "@consta/uikit/IconAdd";
import { useState, useEffect, useRef } from "react";
import { useNavigate } from "react-router-dom";
import { LoginModal } from "../Modals/LoginModal/LoginModal";
import { RegisterModal } from "../Modals/RegisterModal/RegisterModal";
import { Auth } from "../../lib/ApiClient";
import { User } from "@consta/uikit/User";
import { b64toBlob } from "../../lib/Helpers";
import { ContextMenu } from "@consta/uikit/ContextMenu";
import { IconArrowDown } from "@consta/uikit/IconArrowDown";
import { IconCommentStroked } from "@consta/uikit/IconCommentStroked";
import { IconDocFilled } from "@consta/uikit/IconDocFilled";
import { IconExit } from "@consta/uikit/IconExit";
import { useForceUpdate } from "../../hooks/useForceUpdate";
import Inversia from "../../Inversia.svg";

export default function EngineHeader() {
  const update = useForceUpdate();
  const [isLoginModalOpen, setIsLoginModalOpen] = useState(false);
  const [isRegisterModalOpen, setIsRegisterModalOpen] = useState(false);
  const [user, setUser] = useState(null);
  const [menu, setMenu] = useState(false);
  const [active, setActive] = useState(0);
  const navigate = useNavigate();
  const adminItems = [
    {
      label: "Пользователи",
      onClick: () => navigate("/users"),
      rightIcon: IconUser,
    },
    {
      label: "Выпуски",
      onClick: () => navigate("/issues"),
      rightIcon: IconDocFilled,
    },
    {
      label: "Статьи",
      onClick: () => navigate("/articles"),
      rightIcon: IconDocFilled,
    },
    {
      label: "Комментарии",
      onClick: () => navigate("/comments"),
      rightIcon: IconCommentStroked,
    },
    {
      label: "Выйти",
      onClick: () => {
        Auth.exit();
        setUser(null);
        window.location.reload();
      },
      rightIcon: IconExit,
    },
  ];
  const userItems = [
    {
      label: "Выйти",
      onClick: () => {
        Auth.exit();
        setUser(null);
        window.location.reload();
      },
      rightIcon: IconExit,
    },
  ];

  const menuItems = Array.from([
    {
      label: "Главная",
      onClick: () => {
        setActive(0);
        update();
      },
    },
    {
      label: "Выпуски",
      onClick: () => {
        setActive(1);
        update();
      },
    },
    {
      label: "Фотогалерея",
      onClick: () => {
        setActive(2);
        update();
      },
    },
    {
      label: "Школа журналистики",
      onClick: () => {
        setActive(3);
        update();
      },
    },
    {
      label: "О журнале",
      onClick: () => {
        setActive(4);
        update();
      },
    },
  ]).map((x, i) =>
    i === active
      ? { label: x.label, onClick: x.onClick, active: true }
      : { label: x.label, onClick: x.onClick }
  );

  const ref = useRef(null);

  useEffect(() => {
    !user &&
      Auth.me()
        .then((res) => {
          setUser(res.data);
        })
        .catch((err) => {});
  }, [user]);

  function renderRight() {
    if (user == null) {
      return (
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
              onClick={() => setIsRegisterModalOpen(true)}
            />
          </HeaderModule>
        </>
      );
    } else {
      switch (user.role) {
        case 2: {
          return (
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
                  onClick={() => setIsRegisterModalOpen(true)}
                />
              </HeaderModule>
            </>
          );
        }
        case 1: {
          return (
            <>
              <HeaderModule indent="l">
                <User
                  avatarUrl={
                    user.image && URL.createObjectURL(b64toBlob(user.image))
                  }
                  name={user.nickname}
                  size="l"
                />
              </HeaderModule>

              <HeaderModule indent="s">
                <Button
                  style={{ zIndex: 999 }}
                  view="clear"
                  form="round"
                  ref={ref}
                  iconRight={IconArrowDown}
                  onClick={() => setMenu(!menu)}
                />
                <ContextMenu
                  isOpen={menu}
                  offset="xs"
                  size="m"
                  direction="downCenter"
                  items={userItems}
                  getItemLabel={(item) => item.label}
                  getItemRightIcon={(item) => item.rightIcon}
                  anchorRef={ref}
                />
              </HeaderModule>
            </>
          );
        }
        case 0: {
          return (
            <>
              <HeaderModule indent="l">
                <Button
                  size="m"
                  view="secondary"
                  form="round"
                  label="Опубликовать статью"
                  iconLeft={IconAdd}
                  onClick={() => navigate("/publish")}
                />
              </HeaderModule>
              <HeaderModule indent="l">
                <Button
                  size="m"
                  view="primary"
                  form="round"
                  label="Выпустить"
                  iconLeft={IconAdd}
                  onClick={() => navigate("/publish_issue")}
                />
              </HeaderModule>
              <HeaderModule indent="l">
                <User
                  avatarUrl={
                    user.image && URL.createObjectURL(b64toBlob(user.image))
                  }
                  name={user.nickname}
                  size="l"
                />
              </HeaderModule>

              <HeaderModule indent="s">
                <Button
                  style={{ zIndex: 999 }}
                  view="clear"
                  form="round"
                  ref={ref}
                  iconRight={IconArrowDown}
                  onClick={() => setMenu(!menu)}
                />
                <ContextMenu
                  isOpen={menu}
                  offset="xs"
                  size="m"
                  direction="downCenter"
                  items={adminItems}
                  getItemLabel={(item) => item.label}
                  getItemRightIcon={(item) => item.rightIcon}
                  anchorRef={ref}
                />
              </HeaderModule>
            </>
          );
        }
      }
    }
  }

  return (
    <Theme className="App" preset={presetGpnDefault}>
      <Header
        style={{
          padding: 10,
          height: 80,
        }}
        leftSide={
          <>
            <HeaderModule>
              <img
                style={{
                  display: "inline-block",
                  cursor: "pointer",
                  height: 100,
                  marginLeft: 60,
                  marginRight: 0,
                }}
                src={Inversia}
                fill="white"
                onClick={() => navigate("/")}
              />
            </HeaderModule>
            <HeaderModule>
              <Theme className="App" preset={presetGpnDefault}>
                <HeaderMenu items={menuItems} />
              </Theme>
            </HeaderModule>
          </>
        }
        rightSide={renderRight()}
        className="gradient-h"
      />
      <LoginModal
        isOpen={isLoginModalOpen}
        onClickOutside={() => setIsLoginModalOpen(false)}
        onEsc={() => setIsLoginModalOpen(false)}
      />
      <RegisterModal
        isOpen={isRegisterModalOpen}
        onClickOutside={() => setIsRegisterModalOpen(false)}
        onEsc={() => setIsRegisterModalOpen(false)}
      />
    </Theme>
  );
}
