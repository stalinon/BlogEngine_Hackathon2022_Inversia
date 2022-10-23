import { Theme, presetGpnDefault } from "@consta/uikit/Theme";
import { useParams } from "react-router-dom";
import { useState, useEffect, useRef } from "react";
import { Article } from "../lib/ApiClient";
import { Layout } from "@consta/uikit/Layout";
import { Text } from "@consta/uikit/Text";
import { b64toBlob } from "../lib/Helpers";
import { Tag } from "@consta/uikit/Tag";
import { Button } from "@consta/uikit/Button";
import { ContextMenu } from "@consta/uikit/ContextMenu";
import { useNavigate } from "react-router-dom";
import { IconEdit } from "@consta/uikit/IconEdit";
import { IconKebab } from "@consta/uikit/IconKebab";
import { IconTrash } from "@consta/uikit/IconTrash";
import { User } from "@consta/uikit/User";

export default function ArticlePage(props) {
  const { id } = useParams();
  const [article, setArticle] = useState(null);
  const [menu, setMenu] = useState(false);
  const navigate = useNavigate();
  const ref = useRef(null);
  const adminItems = [
    {
      label: "Редактировать",
      onClick: () => navigate("/article/edit/" + id),
      rightIcon: IconTrash,
    },
    {
      label: "Удалить",
      onClick: () => {
        Article.delete(id);
      },
      rightIcon: IconEdit,
    },
  ];

  useEffect(() => {
    !article && id && Article.getById(id).then((res) => setArticle(res.data));
  }, [article, id]);

  function renderOptions() {
    return (
      props.Role && (
        <div>
          <Button
            style={{ marginTop: "20px" }}
            view="clear"
            form="round"
            ref={ref}
            iconRight={IconKebab}
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
        </div>
      )
    );
  }

  function renderTopBlock() {
    return (
      <Layout direction="column">
        <Layout direction="column" className="title-subtitle">
          <Layout style={{ justifyContent: "space-between" }}>
            <Text as="h1" cursor="pointer" size="3xl" weight="bold">
              {article.header}
            </Text>
            {renderOptions()}
          </Layout>
          <Layout indent="l" style={{ justifyContent: "space-between" }}>
            <Layout>
              {article.tags.map((t, i) => {
                return (
                  <Tag
                    key={i}
                    group={i}
                    mode="cancel"
                    label={t}
                    style={{ margin: 5 }}
                  />
                );
              })}
            </Layout>
            <Layout direction="column">
              <Text view="ghost" size="s">
                {article.author.first_name + " " + article.author.last_name}
              </Text>
              <Text view="ghost" size="s">
                {new Date(article.created).toLocaleString("ru", {
                  year: "numeric",
                  month: "numeric",
                  day: "numeric",
                  timezone: "UTC",
                })}
              </Text>
            </Layout>
          </Layout>
        </Layout>
        <div>&nbsp;</div>
        <Layout className="desc">{article.desc}</Layout>
        <div>&nbsp;</div>
        <Layout className="image">
          <img
            style={{ width: "100%" }}
            src={URL.createObjectURL(b64toBlob(article.leading_image))}
          />
        </Layout>
        <div>&nbsp;</div>
      </Layout>
    );
  }

  function renderTextArticle() {
    return <div dangerouslySetInnerHTML={{ __html: article.text }} />;
  }

  function renderCommentsBlock(comments) {
    return comments.map((x, i) => (
      <Layout direction="row" key={i}>
        <Layout flex={1}>
          <User
            avatarUrl={x.user.image}
            size="s"
            name={x.user.nickname}
            info={x.user.first_name + " " + x.user.last_name}
          />
        </Layout>
        <Layout flex={3}>
          <Text>x.text</Text>
        </Layout>
      </Layout>
    ));
  }

  function renderSendCommentBlock() {
    return <Layout></Layout>;
  }

  return (
    <Theme className="App" preset={presetGpnDefault}>
      <div
        style={{
          marginLeft: 200,
          marginRight: 200,
          marginBottom: 30,
          marginTop: 30,
          padding: 30,
          background: "rgba(256, 256, 256, 0.8)",
          boxShadow: "rgba(149, 157, 165, 0.2) 0px 8px 24px",
        }}
      >
        {article && renderTopBlock()}
        {article && renderTextArticle()}
        {article && renderCommentsBlock(article.comments)}
      </div>
    </Theme>
  );
}
