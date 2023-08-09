import { Formik } from "formik";
import MDBox from "components/MDBox";
import MDButton from "components/MDButton";
import Card from "@mui/material/Card";
import Grid from "@mui/material/Grid";
import MDTypography from "components/MDTypography";
import FormField from "components/Customized/FormFiled";
import useBankAccount from "./hooks/useBankAccount";
import { FormLabel } from "@mui/material";
import ReactDatePicker from "react-datepicker";
import moment from "moment";

const BankAccountForm = () => {
  const { initialValues, bankAccountSchema, handleSubmitForm, handleCancel } = useBankAccount();

  return (
    <Formik
      enableReinitialize
      initialValues={initialValues}
      validationSchema={bankAccountSchema}
      onSubmit={(values) => {
        const data = { ...values };

        handleSubmitForm(data);
      }}
    >
      {({ values, errors, handleChange, handleSubmit, setFieldValue }) => (
        <>
          <Grid item xs={12} md={5} sx={{ textAlign: "right", paddingBottom: 1 }}>
            <MDButton
              onClick={handleCancel}
              variant="gradient"
              color="error"
              sx={{ marginRight: 1 }}
            >
              Cancel
            </MDButton>
            <MDButton onClick={handleSubmit} variant="gradient" color="info">
              Save Changed
            </MDButton>
          </Grid>

          <MDBox height="80vh" mb={2} className={"ag-theme-alpine"}>
            <Card id="basic-info" sx={{ overflow: "visible" }}>
              <MDBox p={3}>
                <MDTypography variant="h5">Bank Account Form</MDTypography>
              </MDBox>

              <MDBox component="form" pb={3} px={3}>
                <Grid container spacing={1}>
                  <Grid item xs={12} sm={6}>
                    <FormField
                      value={values.bankName}
                      name="bankName"
                      label="Bank name"
                      onChange={handleChange}
                      error={errors.bankName}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FormField
                      value={values.accountHolderName}
                      name="accountHolderName"
                      label="Account holder name"
                      onChange={handleChange}
                      error={errors.accountHolderName}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FormField
                      value={values.BSBNumber}
                      name="BSBNumber"
                      label="BSB Number"
                      onChange={handleChange}
                      error={errors.BSBNumber}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FormField
                      value={values.accountNumber}
                      name="accountNumber"
                      label="Account number"
                      onChange={handleChange}
                      error={errors.accountNumber}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FormField
                      value={values.emailAddress}
                      name="emailAddress"
                      label="Email"
                      onChange={handleChange}
                      error={errors.emailAddress}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FormField
                      value={values.phoneNumber}
                      name="phoneNumber"
                      label="Phone number"
                      onChange={handleChange}
                      error={errors.phoneNumber}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FormField
                      value={values.nameApplicant}
                      name="nameApplicant"
                      label="Applicant name"
                      onChange={handleChange}
                      error={errors.nameApplicant}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FormField
                      value={values.signature}
                      name="signature"
                      label="Signature"
                      onChange={handleChange}
                      error={errors.signature}
                    />
                  </Grid>
                  <Grid item xs={12} sx={{ display: "flex", flexDirection: "column" }} sm={6}>
                    <FormLabel sx={{ fontSize: "0.8rem" }}>Sign Date</FormLabel>
                    <div style={{ width: "100%", height: "100%" }}>
                      <ReactDatePicker
                        name="signDate"
                        selected={moment(values.signDate).toDate()}
                        onChange={(e) => {
                          setFieldValue("signDate", moment(e).toDate());
                        }}
                      />
                    </div>
                  </Grid>
                </Grid>
              </MDBox>
            </Card>
          </MDBox>
        </>
      )}
    </Formik>
  );
};

export default BankAccountForm;
