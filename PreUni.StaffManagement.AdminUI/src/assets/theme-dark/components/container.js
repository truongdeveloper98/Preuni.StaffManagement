/**
=========================================================
* Otis Admin PRO - v2.0.1
=========================================================

* Product Page: https://material-ui.com/store/items/otis-admin-pro-material-dashboard-react/
* Copyright 2022 Creative Tim (https://www.creative-tim.com)

Coded by www.creative-tim.com

 =========================================================

* The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
*/

// Otis Admin PRO React base styles
import breakpoints from "assets/theme-dark/base/breakpoints";

// Otis Admin PRO React helper functions
import pxToRem from "assets/theme-dark/functions/pxToRem";

const {
  values: { sm, md, lg, xl, xxl },
} = breakpoints;

const SM = `@media (min-width: ${sm}px)`;
const MD = `@media (min-width: ${md}px)`;
const LG = `@media (min-width: ${lg}px)`;
const XL = `@media (min-width: ${xl}px)`;
const XXL = `@media (min-width: ${xxl}px)`;

const sharedClasses = {
  paddingRight: `${pxToRem(24)} !important`,
  paddingLeft: `${pxToRem(24)} !important`,
  marginRight: "auto !important",
  marginLeft: "auto !important",
  width: "100% !important",
  position: "relative",
};

export default {
  [SM]: {
    ".MuiContainer-root": {
      ...sharedClasses,
      maxWidth: "540px !important",
    },
  },
  [MD]: {
    ".MuiContainer-root": {
      ...sharedClasses,
      maxWidth: "720px !important",
    },
  },
  [LG]: {
    ".MuiContainer-root": {
      ...sharedClasses,
      maxWidth: "960px !important",
    },
  },
  [XL]: {
    ".MuiContainer-root": {
      ...sharedClasses,
      maxWidth: "1140px !important",
    },
  },
  [XXL]: {
    ".MuiContainer-root": {
      ...sharedClasses,
      maxWidth: "1320px !important",
    },
  },
};
