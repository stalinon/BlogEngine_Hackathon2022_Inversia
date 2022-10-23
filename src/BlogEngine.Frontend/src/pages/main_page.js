import { Theme, presetGpnDefault } from "@consta/uikit/Theme";
import { Layout } from "@consta/uikit/Layout";
import { Article } from "../lib/ApiClient";
import { useState, useEffect } from "react";
import { NewsHeaderCard } from "react-ui-cards";
import { useNavigate } from "react-router-dom";
import { b64toBlob } from "../lib/Helpers";
import { Pagination } from "@consta/uikit/Pagination";

export default function MainPage() {
  const [articles, setArticles] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const navigate = useNavigate();
  useEffect(() => {
    !articles[0] &&
      Article.getPaged(currentPage, 6).then((res) => {
        setArticles(res.data.items);
        setTotalPages(Math.ceil(res.data.totalCount / 6));
        setCurrentPage(res.data.page);
        console.log("FFFFFFFFF" + totalPages + " " + currentPage);
      });
  }, [currentPage, articles]);
  return (
    articles[0] && (
      <Theme className="App" preset={presetGpnDefault}>
        <div className="users-page" style={{ margin: 50 }}>
          <Layout>
            {articles.map((article, i) => (
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
            ))}
          </Layout>
          <Layout>
            <Pagination
              currentPage={currentPage}
              onChange={(value) => setCurrentPage(value)}
              totalPages={totalPages}
              size="s"
              position="center"
            />
          </Layout>
        </div>
      </Theme>
    )
  );
}
