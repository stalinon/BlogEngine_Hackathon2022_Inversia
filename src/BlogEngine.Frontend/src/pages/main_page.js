import { Theme, presetGpnDefault } from "@consta/uikit/Theme";
import { Layout } from "@consta/uikit/Layout";
import { Article, Issue } from "../lib/ApiClient";
import { useState, useEffect } from "react";
import { NewsHeaderCard } from "react-ui-cards";
import { useNavigate } from "react-router-dom";
import { b64toBlob } from "../lib/Helpers";
import { Pagination } from "@consta/uikit/Pagination";
import { Text } from "@consta/uikit/Text";

export default function MainPage() {
  const [articles, setArticles] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);

  const [issues, setIssues] = useState([]);
  const [currentPage2, setCurrentPage2] = useState(1);
  const [totalPages2, setTotalPages2] = useState(1);

  const navigate = useNavigate();

  useEffect(() => {
    !articles[0] &&
      Article.getPaged(currentPage, 6).then((res) => {
        setArticles(res.data.items);
        setTotalPages(Math.ceil(res.data.totalCount / 6));
        setCurrentPage(res.data.page);
      });
  }, [currentPage, articles]);

  useEffect(() => {
    !issues[0] &&
      Issue.getPaged(currentPage2, 4).then((res) => {
        setIssues(res.data.items);
        setTotalPages2(Math.ceil(res.data.totalCount / 4));
        setCurrentPage2(res.data.page);
      });
  }, [currentPage2, issues]);

  return (
    articles[0] &&
    issues[0] && (
      <Theme className="App" preset={presetGpnDefault}>
        <Layout style={{ margin: 50 }}>
          <Layout
            style={{ display: "flex", flexWrap: "wrap" }}
            flex={5}
            direction="column"
            className="articles-page"
          >
            <div style={{ display: "flex", flexWrap: "wrap" }}>
              {articles.map((article, i) => (
                <div
                  key={i}
                  style={{
                    marginTop: 15,
                    marginBottom: 15,
                    marginLeft: 5,
                    marginRight: 5,
                  }}
                >
                  <NewsHeaderCard
                    key={i}
                    className="news_card"
                    onClick={() => navigate("/article/" + article.id)}
                    thumbnail={URL.createObjectURL(
                      b64toBlob(article.leading_image)
                    )}
                    author={
                      article.author.first_name + " " + article.author.last_name
                    }
                    date={new Date(article.created).toLocaleString("ru", {
                      year: "numeric",
                      month: "numeric",
                      day: "numeric",
                      timezone: "UTC",
                    })}
                    title={article.header}
                    tags={article.tags.slice(0, 3)}
                  />
                </div>
              ))}
            </div>
            <Layout>
              <Pagination
                currentPage={currentPage}
                onChange={(value) => setCurrentPage(value)}
                totalPages={totalPages}
                size="s"
                position="center"
              />
            </Layout>
          </Layout>
          <Layout
            direction="column"
            flex={1}
            className="issues-page gradient"
            style={{
              paddingBottom: 50,
              borderRadius: 10,
              marginTop: 21,
            }}
          >
            <Text
              as="h2"
              weight="bold"
              size="xl"
              align="center"
              style={{ marginLeft: 16, color: "white" }}
            >
              Выпуски
            </Text>
            <div
              style={{
                display: "flex",
                flexWrap: "wrap",
                justifyContent: "center",
              }}
            >
              {issues.map((article, i) => (
                <div key={i} style={{ margin: 10 }}>
                  <NewsHeaderCard
                    key={i}
                    className="news_card"
                    onClick={() => navigate("/issue/" + article.id)}
                    thumbnail={URL.createObjectURL(
                      b64toBlob(article.leading_image)
                    )}
                    date={new Date(article.date).toLocaleString("ru", {
                      year: "numeric",
                      month: "numeric",
                      day: "numeric",
                      timezone: "UTC",
                    })}
                    title={`ИНверсия, выпуск ${article.issue_number}`}
                    style={{ width: "auto", height: "auto" }}
                  />
                </div>
              ))}
            </div>
            <Layout>
              <Pagination
                currentPage={currentPage2}
                onChange={(value) => setCurrentPage2(value)}
                totalPages={totalPages2}
                size="s"
                position="center"
              />
            </Layout>
          </Layout>
        </Layout>
      </Theme>
    )
  );
}
