import { createSlice } from "@reduxjs/toolkit";

export interface CommonState {
  isLoading: boolean;
  pageSize: number;
  period: any;
}

const initialState: CommonState = {
  isLoading: false,
  pageSize: 10,
  period: undefined,
};

const slice = createSlice({
  name: "common",
  initialState,
  reducers: {
    setLoading: (state, action) => {
      state.isLoading = action.payload;
    },
    setPageSize: (state, action) => {
      state.pageSize = action.payload;
    },
    setPeriod: (state, action) => {
      state.period = action.payload;
    },
  },
});

export const { setLoading, setPageSize, setPeriod } = slice.actions;

export default slice.reducer;
