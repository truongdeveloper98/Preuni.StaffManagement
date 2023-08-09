import { Tab, Tabs } from "@mui/material";
import { useParams } from "react-router-dom";

const JobApplicationDetailTabs = ({ value, handleChange }) => {
  const params = useParams();
  const { id: paramId } = params;

  const a11yProps = (index: number) => {
    return {
      id: `simple-tab-${index}`,
      "aria-controls": `simple-tabpanel-${index}`,
    };
  };

  return (
    <>
      {paramId ? (
        <Tabs value={value} onChange={handleChange} aria-label="basic tabs example">
          <Tab label="Job Application Details" {...a11yProps(0)} />
          <Tab label="Payee" {...a11yProps(1)} />
          <Tab label="Bank Account Detail" {...a11yProps(2)} />
        </Tabs>
      ) : (
        <Tabs value={value} onChange={handleChange} aria-label="basic tabs example">
          <Tab label="Job Application Details" {...a11yProps(0)} />
        </Tabs>
      )}
    </>
  );
};

export default JobApplicationDetailTabs;
