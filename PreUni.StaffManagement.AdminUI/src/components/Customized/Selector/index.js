import React from "react";
import { Autocomplete, TextField } from "@mui/material";

export default function Selector({
  multiple = false,
  hideLabel = false,
  disabled = false,

  options,
  onChange,
  property,
  label,
  ...rest
}) {
  return (
    <Autocomplete
      multiple={multiple}
      id="tags-standard"
      options={options}
      getOptionLabel={(option) => (property ? option[property] ?? option : option)}
      onChange={(e, value) => onChange(value)}
      renderInput={(params) =>
        hideLabel ? (
          <TextField {...params} variant="standard" placeholder={label} />
        ) : (
          <TextField {...params} variant="standard" label={label} placeholder={label} />
        )
      }
      disabled={disabled}
      {...rest}
    />
  );
}
