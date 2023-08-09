import { Action } from "redux";
import {
  persistStore,
  persistReducer,
  FLUSH,
  REHYDRATE,
  PAUSE,
  PERSIST,
  PURGE,
  REGISTER,
} from "redux-persist";
import { ThunkDispatch } from "redux-thunk";
import { configureStore } from "@reduxjs/toolkit";
import storage from "redux-persist/lib/storage";
import rootReducer from "./reducers/index.ts";
import { AuthState } from "./reducers/auth.reducer.ts";
import { CommonState } from "./reducers/common.reducer.ts";
import { SessionState } from "./reducers/session.reducer.ts";
import { UserState } from "./reducers/user.reducer.ts";
import { JobApplicationState } from "./reducers/jobApplication.reducer.ts";

export interface RootState {
  auth: AuthState;
  common: CommonState;
  session: SessionState;
  user: UserState;
  jobApplication: JobApplicationState;
}

const persistConfig = {
  key: "root",
  storage,
  whitelist: ["auth"],
};
const persistedReducer = persistReducer(persistConfig, rootReducer);

export const store = configureStore({
  reducer: persistedReducer,
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware({
      serializableCheck: {
        ignoredActions: [FLUSH, REHYDRATE, PAUSE, PERSIST, PURGE, REGISTER],
      },
    }),
});
export const persistor = persistStore(store);

export type AppDispatch = ThunkDispatch<RootState, void, Action>;
