import { combineReducers } from "redux";
import { LOGOUT } from "constants/actionTypes";

import storage from "redux-persist/lib/storage";
import auth from "./auth.reducer";
import common from "./common.reducer";
import user from "./user.reducer";
import session from "./session.reducer";
import jobApplication from "./jobApplication.reducer";

const appReducer = combineReducers({
  auth,
  common,
  user,
  session,
  jobApplication,
});

const rootReducer = (state, action) => {
  if (action.type === LOGOUT) {
    storage.removeItem("persist:root");
    return appReducer(undefined, action);
  }
  return appReducer(state, action);
};

export default rootReducer;
