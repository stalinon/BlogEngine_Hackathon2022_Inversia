import EngineHeader from "../components/EngineHeader/EngineHeader";
import { Outlet } from "react-router-dom";

export default function BasePage() {
  return (
    <div className="base-page">
      <EngineHeader />
      <Outlet />
    </div>
  );
}
