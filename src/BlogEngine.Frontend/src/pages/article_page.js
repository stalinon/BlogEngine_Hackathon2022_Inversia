import { Theme, presetGpnDefault } from "@consta/uikit/Theme";
import { useParams } from "react-router-dom";
import { useState, useEffect } from "react";
import { Article } from "../lib/ApiClient";
import { Layout } from "@consta/uikit/Layout";
import { Text } from "@consta/uikit/Text";

export default function ArticlePage() {
  const { id } = useParams();
  const [article, setArticle] = useState(null);

  useEffect(
    () =>
      !article && id && Article.getById(id).then((res) => setArticle(res.data)),
    [article, id]
  );

  function renderTopBlock() {
    return (
      <Layout>
        <Layout className="title-subtitle">
          <Text>{article.header}</Text>
          <Text>{article.desc}</Text>
          <Layout>
            <Text>
              {article.author.first_name + " " + article.author.last_name}
            </Text>
            <Text>
              {new Date(article.created).toLocaleString("ru", {
                year: "numeric",
                month: "numeric",
                day: "numeric",
                timezone: "UTC",
              })}
            </Text>
          </Layout>
        </Layout>
        <Layout className="desc"></Layout>
        <Layout className="image"></Layout>
      </Layout>
    );
  }

  return (
    <Theme className="App" preset={presetGpnDefault}>
      {article && renderTopBlock()}
    </Theme>
  );
}
