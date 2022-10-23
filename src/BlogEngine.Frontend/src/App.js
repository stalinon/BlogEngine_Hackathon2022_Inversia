import { BrowserRouter, Routes, Route } from "react-router-dom";
import BasePage from "./pages/base_page";
import UsersPage from "./pages/users_page";
import { useState, useEffect } from "react";
import { Auth } from "./lib/ApiClient";
import MainPage from "./pages/main_page";
import PublishPage from "./pages/publish_page";
import EditPage from "./pages/edit_page";
import PublishIssuePage from "./pages/publish_issue_page";
import ArticlePage from "./pages/article_page";
import IssuePage from "./pages/issue_page";

function App() {
  const [user, setUser] = useState(null);
  useEffect(() => {
    !user &&
      Auth.me()
        .then((res) => {
          !user && setUser(res.data);
        })
        .catch((err) => {});
  }, [user]);
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<BasePage />}>
          <Route path="/" element={<MainPage />} />
          {user && user.role === 0 && (
            <Route path="users" element={<UsersPage />} />
          )}
          {user && user.role === 0 && (
            <Route path="articles" element={<UsersPage />} />
          )}
          {user && user.role === 0 && (
            <Route path="issues" element={<UsersPage />} />
          )}
          {user && user.role === 0 && (
            <Route path="comments" element={<UsersPage />} />
          )}
          {user && user.role === 0 && (
            <Route path="publish" element={<PublishPage />} />
          )}
          {user && user.role === 0 && (
            <Route path="publish_issue" element={<PublishIssuePage />} />
          )}
          <Route
            path="article/:id"
            element={
              <ArticlePage
                Role={user && user.role === 0}
                UserRole={user && user.role === 1}
              />
            }
          />

          <Route path="issue/:id" element={<IssuePage />} />

          {user && user.role === 0 && (
            <Route path="article/edit/:id" element={<EditPage />} />
          )}
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
