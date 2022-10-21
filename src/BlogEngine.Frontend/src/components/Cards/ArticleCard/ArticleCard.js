import { Theme, presetGpnDefault } from "@consta/uikit/Theme";
import { Card } from "@consta/uikit/Card";
import { Text } from "@consta/uikit/Text";
import { useState, useEffect } from "react";
import { b64toBlob } from "../../../lib/Helpers";
import { Layout } from "@consta/uikit/Layout";
import { useNavigate } from "react-router-dom";

export default function ArticleCard(Article) {
  const [article, setArticle] = useState(null);
  useEffect(() => {
    setArticle(Article.Article);
    console.log(article);
  }, [article, Article]);
  const navigate = useNavigate();
  return (
    <Theme className="App" preset={presetGpnDefault}>
      {article && (
        <Card
          cursor="pointer"
          verticalSpace="s"
          horizontalSpace="l"
          shadow={true}
          onClick={() => navigate(`/article/${article.id}`)}
        >
          <Layout direction="row">
            <Layout flex={1}>
              <Layout>
                <img
                  src={URL.createObjectURL(b64toBlob(article.leading_image))}
                  width="300"
                />
              </Layout>
            </Layout>
            <Layout direction="column" flex={4}>
              <Layout flex={1}>
                <Text cursor="pointer" as="h2" weight="bold" size="xl">
                  {article.header}
                </Text>
              </Layout>
              <Layout flex={10}>
                <Text cursor="pointer" as="p" size="l">
                  {article.desc}
                </Text>
              </Layout>
              <Layout flex={1}>
                <Text as="p" size="s" font="mono" view="ghost">
                  {new Date(Date.parse(article.created)).toLocaleDateString(
                    "ru-RU",
                    {
                      weekday: "long",
                      year: "numeric",
                      month: "long",
                      day: "numeric",
                    }
                  )}
                </Text>
              </Layout>
            </Layout>
          </Layout>
        </Card>
      )}
    </Theme>
  );
}
