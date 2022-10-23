import EditorEditor from "../components/Editor/EditorEditor";
import { useParams } from "react-router-dom";

export default function EditPage() {
  const { id } = useParams();
  return (
    <div className="publish-page" style={{ height: "100%" }}>
      <EditorEditor Header="Исправление статьи" Id={id} />
    </div>
  );
}
