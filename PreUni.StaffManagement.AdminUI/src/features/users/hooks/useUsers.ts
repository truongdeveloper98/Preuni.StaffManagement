import PAGES from "navigation/pages";
import { useRef } from "react";
import { useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { RootState } from "stores";
import { deleteUserRequest } from "../user.services";

const useUsers = () => {
  const navigate = useNavigate();
  const agRef = useRef(null);

  const columnDefs = [
    {
      field: "firstName",
      headerName: "First Name",
    },
    {
      field: "lastName",
      headerName: "Last Name",
    },
    {
      field: "email",
      headerName: "Email",
    },
    {
      field: "phoneNumber",
      headerName: "Phone Number",
    },
    {
      field: "isActive",
      headerName: "Active Status",
    },
  ];

  const users = useSelector((state: RootState) => state.user.users);
  const navigator = useNavigate();

  const handleEditUser = (id: string) => {
    navigate(`${PAGES.editUser}/${id}`);
  };

  const handleCreateUser = () => {
    navigator(PAGES.newUser);
  };

  const handleDeleteUser = async (id: string, callback: any) => {
    await deleteUserRequest(id, callback);
  };

  return {
    // state
    agRef,
    columnDefs,
    users,

    // funcs
    handleEditUser,
    handleCreateUser,
    handleDeleteUser,
  };
};
export default useUsers;
