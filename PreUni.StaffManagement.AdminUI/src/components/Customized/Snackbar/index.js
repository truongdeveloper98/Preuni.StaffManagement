import { useSelector, useDispatch } from "react-redux";
import MDSnackbar from "components/MDSnackbar";
import { reinitialize as userReinitialize } from "stores/reducers/user.reducer";
import { reinitialize as sessionReinitialize } from "stores/reducers/session.reducer";
import { reinitialize as authReinitialize } from "stores/reducers/auth.reducer";
import { reinitialize as jobApplicationReinitialize } from "stores/reducers/jobApplication.reducer";

export default function Snackbar() {
  const dispatch = useDispatch();

  const error = useSelector(
    (state) =>
      state.user.error || state.auth.error || state.session.error || state.jobApplication.error
  );
  const success = useSelector(
    (state) => state.user.success || state.auth.success || state.session.success
  );

  const errorSnackbar = () => (
    <MDSnackbar
      color="error"
      icon="error"
      title="Error"
      content={error?.toString()}
      open={!!error}
      onClose={() => {
        dispatch(userReinitialize());
        dispatch(sessionReinitialize());
        dispatch(authReinitialize());
        dispatch(jobApplicationReinitialize());
      }}
    />
  );

  const successSnackbar = () => (
    <MDSnackbar
      color="success"
      icon="done"
      title="Success"
      open={!!success}
      content={success}
      onClose={() => {
        dispatch(userReinitialize());
        dispatch(sessionReinitialize());
        dispatch(authReinitialize());
        dispatch(jobApplicationReinitialize());
      }}
    />
  );

  return (
    <>
      {errorSnackbar()}
      {successSnackbar()}
    </>
  );
}
