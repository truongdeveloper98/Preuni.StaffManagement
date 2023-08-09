import React from "react";
import { Autocomplete } from "@mui/material";
import MDInput from "components/MDInput";
import MDTypography from "components/MDTypography";

function PageSize({ pageSize, onChange }) {
  return (
    <>
      <Autocomplete
        disableClearable
        value={pageSize}
        options={[5, 10, 15, 20, 25]}
        onChange={onChange}
        size="small"
        sx={{ width: "5rem" }}
        renderInput={(params) => <MDInput {...params} />}
      />
      <MDTypography pr={1} variant="caption" color="secondary">
        &nbsp;&nbsp;entries per page
      </MDTypography>
    </>
  );
}

export default PageSize;
