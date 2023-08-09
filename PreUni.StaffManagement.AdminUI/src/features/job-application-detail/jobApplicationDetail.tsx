/* eslint-disable no-unused-vars */
import React, { useState } from "react";
import BaseLayout from "components/Customized/BaseLayout";
import { Box, Typography } from "@mui/material";
import TFNDeclareForm from "features/tfn-declare/tFNDeclare";
import BankAccountForm from "features/bank-account/bankAccount";
import JobApplicationDetailForm from "./JobApplicationDetailForm";
import JobApplicationDetailTabs from "./JobApplicationDetailTabs";
import "./style.css";

interface TabPanelProps {
  children?: React.ReactNode;
  index: number;
  value: number;
}

function JobApplicationDetail() {
  const [value, setValue] = useState(0);

  const handleChange = (event: React.SyntheticEvent, newValue: number) => {
    setValue(newValue);
  };

  const CustomTabPanel = (props: TabPanelProps) => {
    const { children, value, index, ...other } = props;

    return (
      <div
        role="tabpanel"
        hidden={value !== index}
        id={`simple-tabpanel-${index}`}
        aria-labelledby={`simple-tab-${index}`}
        {...other}
      >
        {value === index && (
          <Box sx={{ p: 3 }}>
            <Typography>{children}</Typography>
          </Box>
        )}
      </div>
    );
  };

  return (
    <BaseLayout>
      <Box sx={{ width: "100%", typography: "body1" }}>
        <Box sx={{ width: "100%" }}>
          <Box sx={{ borderBottom: 1, borderColor: "divider" }}>
            <JobApplicationDetailTabs value={value} handleChange={handleChange} />
          </Box>
          <CustomTabPanel value={value} index={0}>
            <JobApplicationDetailForm />
          </CustomTabPanel>
          <CustomTabPanel value={value} index={1}>
            <TFNDeclareForm />
          </CustomTabPanel>
          <CustomTabPanel value={value} index={2}>
            <BankAccountForm />
          </CustomTabPanel>
        </Box>
      </Box>
    </BaseLayout>
  );
}

export default JobApplicationDetail;
