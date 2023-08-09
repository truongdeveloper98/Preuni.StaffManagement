import AgGridTable from "components/Customized/AgGridTable";
import BaseLayout from "components/Customized/BaseLayout";
import { getUsersRequest } from "./user.services";
import useUsers from "./hooks/useUsers.ts";

function Users() {
  const { agRef, columnDefs, users, handleEditUser, handleCreateUser, handleDeleteUser } =
    useUsers();

  return (
    <BaseLayout>
      <AgGridTable
        ref={agRef}
        columnDefs={columnDefs}
        entity={users}
        onFetching={getUsersRequest}
        onEdit={({ id }) => handleEditUser(id)}
        onCreate={handleCreateUser}
        onDelete={handleDeleteUser}
        entityName="User"
      />
    </BaseLayout>
  );
}

export default Users;
