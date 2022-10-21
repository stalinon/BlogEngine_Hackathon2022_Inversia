import { Table } from "@consta/uikit/Table";
import { useState, useEffect } from "react";
import { User } from "../lib/ApiClient";
import { Theme, presetGpnDefault } from "@consta/uikit/Theme";

export default function UsersPage() {
  const [users, setUsers] = useState([]);
  const columns = [
    { title: "Никнейм", accessor: "nickname" },
    { title: "Имя", accessor: "first_name" },
    { title: "Фамилия", accessor: "last_name" },
    { title: "Дата регистрации", accessor: "created" },
    { title: "Дата обновления", accessor: "updated" },
  ];

  useEffect(() => {
    !users[0] && User.get().then((res) => setUsers(res.data));
  });

  return (
    <Theme className="App" preset={presetGpnDefault}>
      <div className="users-page" style={{ margin: 50 }}>
        <Table
          rows={users}
          columns={columns}
          verticalAlign="top"
          size="m"
          zebraStriped="odd"
          headerVerticalAlign="center"
          borderBetweenRows={true}
          borderBetweenColumns={true}
          getCellWrap={(row) => "break"}
        />
      </div>
    </Theme>
  );
}
