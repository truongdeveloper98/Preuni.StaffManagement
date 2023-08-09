import MDBox from "components/MDBox";
import Card from "@mui/material/Card";
import Grid from "@mui/material/Grid";
import MDTypography from "components/MDTypography";
import { Formik } from "formik";
import MDButton from "components/MDButton";
import * as Yup from "yup";
import FormField from "components/Customized/FormFiled";
import { FormLabel, MenuItem, Select } from "@mui/material";
import useJobApplicationDetail from "./hooks/useJobApplicationDetail";
import { JobApplicationStatus } from "./constants";
import moment from "moment";
import ReactDatePicker from "react-datepicker";
import React from "react";

interface FormValues {
  jobTitle: string;
  firstName: string;
  lastName: string;
  status: string;
  appliedDate: string;
  email: string;
  phoneNumber?: string;
}

const JobApplicationSchema = Yup.object().shape({
  jobTitle: Yup.string().required("Job title is required"),
  firstName: Yup.string().min(2, "Too Short!").max(50, "Too Long!").required("Required"),
  lastName: Yup.string().min(2, "Too Short!").max(50, "Too Long!").required("Required"),
  email: Yup.string().email("Invalid Email").required("Email is required"),
  phoneNumber: Yup.string(),
  status: Yup.string(),
  appliedDate: Yup.string(),
});

const JobApplicationDetailForm = () => {
  const { jobApplication, handleCancel, handleSubmitForm, paramId } = useJobApplicationDetail();

  return (
    <Formik
      enableReinitialize
      initialValues={
        {
          jobTitle: jobApplication?.data?.jobTitle,
          firstName: jobApplication?.data?.firstName,
          lastName: jobApplication?.data?.lastName,
          phoneNumber: jobApplication?.data?.phoneNumber,
          email: jobApplication?.data?.email,
          status: jobApplication?.status || JobApplicationStatus.find((i) => i.value === 1),
          appliedDate: jobApplication?.appliedDate || moment().toString(),
        } as FormValues
      }
      validationSchema={JobApplicationSchema}
      onSubmit={(values) => {
        values.appliedDate = moment(values?.appliedDate).format("YYYY-MM-DD");

        const data = { ...values };

        handleSubmitForm(data);
      }}
      validateOnChange={false}
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
                <MDTypography variant="h5">
                  {paramId ? "Edit Job Application" : "Create Job Application"}
                </MDTypography>
              </MDBox>

              <MDBox component="form" pb={3} px={3}>
                <Grid container spacing={1}>
                  <Grid item xs={12} sm={6}>
                    <FormField
                      value={values.jobTitle}
                      name="jobTitle"
                      label="Job Title"
                      onChange={handleChange}
                      error={errors.jobTitle}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FormField
                      value={values.firstName}
                      name="firstName"
                      label="First Name"
                      onChange={handleChange}
                      error={errors.firstName}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FormField
                      value={values.lastName}
                      name="lastName"
                      label="Last Name"
                      onChange={handleChange}
                      error={errors.lastName}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FormField
                      value={values.email}
                      name="email"
                      label="Email"
                      onChange={handleChange}
                      error={errors.email}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FormField
                      value={values.phoneNumber}
                      name="phoneNumber"
                      label="Phone Number"
                      onChange={handleChange}
                      error={errors.phoneNumber}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FormLabel sx={{ fontSize: "0.8rem" }}>Status</FormLabel>
                    <Select
                      id="status"
                      value={parseInt(values.status)}
                      onChange={(e) => {
                        setFieldValue("status", e.target.value);
                      }}
                      fullWidth
                      style={{ height: "50px" }}
                    >
                      {JobApplicationStatus.map((jApp) => (
                        <MenuItem key={jApp.value} value={jApp.value}>
                          {jApp.label}
                        </MenuItem>
                      ))}
                    </Select>
                  </Grid>
                  <Grid item xs={12} sx={{ display: "flex", flexDirection: "column" }} sm={6}>
                    <FormLabel sx={{ fontSize: "0.8rem" }}>Applied Date</FormLabel>
                    <div style={{ width: "100%", height: "100%" }}>
                      <ReactDatePicker
                        name="appliedDate"
                        selected={moment(values.appliedDate).toDate()}
                        onChange={(e) => {
                          setFieldValue("appliedDate", moment(e).toDate());
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

export default React.memo(JobApplicationDetailForm);
