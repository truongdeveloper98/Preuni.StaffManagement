import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { RootState } from "stores";
import { useSelector } from "react-redux";
import PAGES from "navigation/pages";
import { loginRequest } from "../sign-in.services";

const useSignIn = () => {
  const navigate = useNavigate();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const token = useSelector((state: RootState) => state.auth.token);

  const handleLogin = () => {
    loginRequest({ email, password });
  };

  useEffect(() => {
    if (token) {
      navigate(PAGES.users);
    }
  }, [token]);

  return {
    state: {
      email,
      setEmail,
      password,
      setPassword,
    },

    function: {
      handleLogin,
    },
  };
};
export default useSignIn;
