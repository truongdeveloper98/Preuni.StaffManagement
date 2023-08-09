/* eslint-disable no-unused-vars */
import React from "react";
import MDBox from "components/MDBox";
import Card from "@mui/material/Card";
import Grid from "@mui/material/Grid";
import MDTypography from "components/MDTypography";
import { Formik } from "formik";
import { useMaterialUIController } from "context";
import MDButton from "components/MDButton";
import { useParams } from "react-router-dom";
import * as Yup from "yup";
import REG_EXP from "constants/regExp";
import FormField from "components/Customized/FormFiled";
import BaseLayout from "components/Customized/BaseLayout";
import Checkbox from "@mui/material/Checkbox";
import { FormControlLabel, FormGroup, FormHelperText } from "@mui/material";
import "./style.css";
import useUserDetail from "./hooks/useUserDetail";

interface FormValues {
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber?: string;
  roles: string[];
}

const UserSchema = Yup.object().shape({
  firstName: Yup.string().min(2, "Too Short!").max(50, "Too Long!").required("Required"),
  lastName: Yup.string().min(2, "Too Short!").max(50, "Too Long!").required("Required"),
  email: Yup.string().email("Invalid email").required("Required"),
  phoneNumber: Yup.string().matches(REG_EXP.phone, "Phone number is not valid"),
  roles: Yup.array().of(Yup.string()).min(1),
});

function UserDetail() {
  const [controller] = useMaterialUIController();
  const { darkMode } = controller;
  const params = useParams();
  const { user, handleCancel, handleSubmitForm } = useUserDetail();

  return (
    <BaseLayout>
      <Formik
        enableReinitialize
        initialValues={
          {
            firstName: user?.data?.firstName,
            lastName: user?.data?.lastName,
            email: user?.data?.email,
            phoneNumber: user?.data?.phoneNumber,
            roles: user?.data?.roles,
          } as FormValues
        }
        validationSchema={UserSchema}
        onSubmit={(values) => {
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
            <MDBox
              height="80vh"
              mb={2}
              className={darkMode ? "ag-theme-alpine-dark" : "ag-theme-alpine"}
            >
              <Card id="basic-info" sx={{ overflow: "visible" }}>
                <MDBox p={3}>
                  <MDTypography variant="h5">
                    {params?.id ? "Edit User" : "Create User"}
                  </MDTypography>
                </MDBox>

                <MDBox component="form" pb={3} px={3}>
                  <Grid container spacing={1}>
                    <Grid item xs={12} sm={6}>
                      <FormField
                        value={values.firstName}
                        name="firstName"
                        label="First Name"
                        placeholder="Alec"
                        onChange={handleChange}
                        error={errors.firstName}
                      />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                      <FormField
                        value={values.lastName}
                        name="lastName"
                        label="Last Name"
                        placeholder="Thompson"
                        onChange={handleChange}
                        error={errors.lastName}
                      />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                      <FormField
                        value={values.email}
                        name="email"
                        label="Email"
                        placeholder="example@email.com"
                        inputProps={{ type: "email" }}
                        onChange={handleChange}
                        error={errors.email}
                      />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                      <FormField
                        value={values.phoneNumber}
                        name="phoneNumber"
                        label="Phone Number"
                        placeholder="+40 735 631 620"
                        onChange={handleChange}
                        error={errors.phoneNumber}
                      />
                    </Grid>
                    {(values.roles || !params?.id) && (
                      <Grid item xs={12} sm={6}>
                        <FormGroup>
                          <FormControlLabel
                            checked={values.roles?.includes("Administrator")}
                            control={<Checkbox size="small" />}
                            label="Administrator"
                            onChange={() => {
                              let roles = values.roles ? [...values.roles] : [];
                              if (roles.includes("Administrator")) {
                                roles = roles.filter((r) => r !== "Administrator");
                              } else {
                                roles.push("Administrator");
                              }
                              setFieldValue("roles", roles);
                            }}
                          />
                          <FormControlLabel
                            checked={values.roles?.includes("Applicant")}
                            control={<Checkbox size="small" />}
                            label="Applicant"
                            onChange={() => {
                              let roles = values.roles ? [...values.roles] : [];
                              if (roles.includes("Applicant")) {
                                roles = roles.filter((r) => r !== "Applicant");
                              } else {
                                roles.push("Applicant");
                              }
                              setFieldValue("roles", roles);
                            }}
                          />
                        </FormGroup>
                        {errors?.roles && <FormHelperText error>{errors?.roles}</FormHelperText>}
                      </Grid>
                    )}
                  </Grid>
                </MDBox>
              </Card>
            </MDBox>
          </>
        )}
      </Formik>
    </BaseLayout>
  );
}

export default UserDetail;
