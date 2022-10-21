import ArticleCard from "../components/Cards/ArticleCard/ArticleCard";
import { Article } from "../lib/ApiClient";
import { useState, useEffect } from "react";

export default function MainPage() {
  const [article, setArticle] = useState(null);
  useEffect(() => {
    !article && Article.getById(1).then((res) => setArticle(res.data));
  }, [article]);
  return (
    <div className="users-page" style={{ margin: 50 }}>
      <ArticleCard Article={article} />
    </div>
  );
}
