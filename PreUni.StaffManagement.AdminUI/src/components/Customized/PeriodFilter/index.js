import React from "react";
import MDBox from "components/MDBox";
import { Card } from "@mui/material";
import MDButton from "components/MDButton";
import MDTypography from "components/MDTypography";
import usePeriodFilter from "./hooks/usePeriodFilter";
import { filters } from "./constants";

function PeriodFiltersCard() {
  const { filter, handleFilter } = usePeriodFilter();

  return (
    <MDBox>
      <Card>
        <MDBox p={2} pb={0}>
          <MDTypography variant="h6" fontWeight="medium">
            Patients Filter
          </MDTypography>
        </MDBox>
        <MDBox overflow="auto" flexDirection="row" p={2}>
          {filters.map(({ title, value }) => (
            <MDBox display="inline-block" m={0.5}>
              <MDButton
                onClick={() => handleFilter(title, value)}
                size="small"
                variant={filter === title ? "contained" : "outlined"}
                color="info"
              >
                {title}
              </MDButton>
            </MDBox>
          ))}
        </MDBox>
      </Card>
    </MDBox>
  );
}

export default PeriodFiltersCard;
