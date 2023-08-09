import { Formik } from "formik";
import { WorkType } from "./constants";
import MDBox from "components/MDBox";
import MDButton from "components/MDButton";
import Card from "@mui/material/Card";
import Grid from "@mui/material/Grid";
import MDTypography from "components/MDTypography";
import FormField from "components/Customized/FormFiled";
import { FormControlLabel, FormLabel, MenuItem, Radio, RadioGroup, Select } from "@mui/material";
import useTFNDeclare from "./hooks/useTFNDeclare";
import ReactDatePicker from "react-datepicker";
import moment from "moment";

const TFNDeclareForm = () => {
  const { initialValues, bankAccountSchema, handleSubmitForm, handleCancel } = useTFNDeclare();

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
                <MDTypography variant="h5">Payee Form</MDTypography>
              </MDBox>

              <MDBox component="form" pb={3} px={3}>
                <Grid container spacing={1}>
                  <Grid item xs={12} sm={6}>
                    <FormField
                      value={values.taxNumber}
                      name="taxNumber"
                      label="Tax Number"
                      onChange={handleChange}
                      error={errors.taxNumber}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FormField
                      value={values.nameTitle}
                      name="nameTitle"
                      label="Name Title"
                      onChange={handleChange}
                      error={errors.nameTitle}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FormField
                      value={values.surName}
                      name="surName"
                      label="Sur name"
                      onChange={handleChange}
                      error={errors.surName}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FormField
                      value={values.firstName}
                      name="firstName"
                      label="First name"
                      onChange={handleChange}
                      error={errors.firstName}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FormField
                      value={values.otherName}
                      name="otherName"
                      label="Other name"
                      onChange={handleChange}
                      error={errors.otherName}
                    />
                  </Grid>
                  <Grid item xs={12} sx={{ display: "flex", flexDirection: "column" }} sm={6}>
                    <FormLabel sx={{ fontSize: "0.8rem" }}>Date of birth</FormLabel>
                    <div style={{ width: "100%", height: "100%" }}>
                      <ReactDatePicker
                        name="dateOfBirth"
                        dateFormat="yyyy-MM-dd"
                        selected={
                          values?.yearOfBirth && values?.monthOfBirth && values?.dayOfBirth
                            ? moment(
                                `${values?.yearOfBirth}/${values?.monthOfBirth}/${values?.dayOfBirth}`
                              ).toDate()
                            : moment().toDate()
                        }
                        onChange={(e) => {
                          const selectedDate = moment(e);
                          setFieldValue("dayOfBirth", selectedDate.date());
                          setFieldValue("monthOfBirth", selectedDate.month() + 1);
                          setFieldValue("yearOfBirth", selectedDate.year());
                        }}
                      />
                    </div>
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FormField
                      value={values.taxNumberOption}
                      name="taxNumberOption"
                      label="Tax number option"
                      onChange={handleChange}
                      error={errors.taxNumberOption}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FormField
                      value={values.taxNumberOption}
                      name="taxNumberOption"
                      label="Tax number option"
                      onChange={handleChange}
                      error={errors.taxNumberOption}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FormField
                      value={values.previousLastName}
                      name="previousLastName"
                      label="Previous last name"
                      onChange={handleChange}
                      error={errors.previousLastName}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FormField
                      value={values.address}
                      name="address"
                      label="Address"
                      onChange={handleChange}
                      error={errors.address}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FormField
                      value={values.suburb}
                      name="suburb"
                      label="Suburb"
                      onChange={handleChange}
                      error={errors.suburb}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FormField
                      value={values.state}
                      name="state"
                      label="State"
                      onChange={handleChange}
                      error={errors.state}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FormField
                      value={values.postCode}
                      name="postCode"
                      label="Post Code"
                      onChange={handleChange}
                      error={errors.postCode}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FormLabel sx={{ fontSize: "0.8rem" }}>Work Type</FormLabel>
                    <Select
                      id="workType"
                      value={parseInt(values.workType)}
                      onChange={(e) => {
                        setFieldValue("workType", e.target.value);
                      }}
                      fullWidth
                      style={{ height: "50%" }}
                    >
                      {WorkType.map((wt) => (
                        <MenuItem key={wt.value} value={wt.value}>
                          {wt.label}
                        </MenuItem>
                      ))}
                    </Select>
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
                  <Grid item xs={12} sm={6}>
                    <FormLabel id="demo-radio-buttons-group-label">Is Autralia Resident</FormLabel>
                    <RadioGroup
                      aria-labelledby="demo-radio-buttons-group-label"
                      value={values?.isAustraliaResident || false}
                      name="radio-buttons-group"
                      onChange={(e) => setFieldValue("isAustraliaResident", e.target.value)}
                    >
                      <FormControlLabel value="true" control={<Radio />} label="True" />
                      <FormControlLabel
                        defaultChecked
                        value="false"
                        control={<Radio />}
                        label="False"
                      />
                    </RadioGroup>
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FormLabel id="demo-radio-buttons-group-label">Is Tax Fee</FormLabel>
                    <RadioGroup
                      aria-labelledby="demo-radio-buttons-group-label"
                      value={values.isTaxFee || false}
                      name="radio-buttons-group"
                      onChange={(e) => setFieldValue("isTaxFee", e.target.value)}
                    >
                      <FormControlLabel value={true} control={<Radio />} label="True" />
                      <FormControlLabel
                        defaultChecked
                        value={false}
                        control={<Radio />}
                        label="False"
                      />
                    </RadioGroup>
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FormLabel id="demo-radio-buttons-group-label">
                      Is Tax Offset For Pensioners
                    </FormLabel>
                    <RadioGroup
                      aria-labelledby="demo-radio-buttons-group-label"
                      value={values?.isTaxOffsetForPensioners || false}
                      name="radio-buttons-group"
                      onChange={(e) => setFieldValue("isTaxOffsetForPensioners", e.target.value)}
                    >
                      <FormControlLabel value={true} control={<Radio />} label="True" />
                      <FormControlLabel
                        defaultChecked
                        value={false}
                        control={<Radio />}
                        label="False"
                      />
                    </RadioGroup>
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FormLabel id="demo-radio-buttons-group-label">Is Education Loan</FormLabel>
                    <RadioGroup
                      aria-labelledby="demo-radio-buttons-group-label"
                      value={values?.isEducationLoan || false}
                      name="radio-buttons-group"
                      onChange={(e) => setFieldValue("isEducationLoan", e.target.value)}
                    >
                      <FormControlLabel value={true} control={<Radio />} label="True" />
                      <FormControlLabel
                        defaultChecked
                        value={false}
                        control={<Radio />}
                        label="False"
                      />
                    </RadioGroup>
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FormLabel id="demo-radio-buttons-group-label">
                      Is Financial Supplement Support
                    </FormLabel>
                    <RadioGroup
                      aria-labelledby="demo-radio-buttons-group-label"
                      value={values?.isFinancialSupplementSupport || false}
                      name="radio-buttons-group"
                      onChange={(e) =>
                        setFieldValue("isFinancialSupplementSupport", e.target.value)
                      }
                    >
                      <FormControlLabel value={true} control={<Radio />} label="True" />
                      <FormControlLabel
                        defaultChecked
                        value={false}
                        control={<Radio />}
                        label="False"
                      />
                    </RadioGroup>
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

export default TFNDeclareForm;
