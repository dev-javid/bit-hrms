import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { AuthTokens, saveAuthTokens } from '../types';
import { authApi } from '../rtk/rtk.auth';

interface State {
  errorMessage: string | null;
  refreshingToken: boolean;
  sessionActive: boolean;
}

const initialState: State = {
  errorMessage: null,
  refreshingToken: false,
  sessionActive: false,
};

const slice = createSlice({
  name: 'error',
  initialState,
  reducers: {
    setError(state, action: PayloadAction<string>) {
      state.errorMessage = action.payload;
    },
    clearError(state) {
      state.errorMessage = null;
    },
    setRefreshingToken(state, action: PayloadAction<boolean>) {
      state.refreshingToken = action.payload;
    },
    setUser: (state, action: PayloadAction<AuthTokens>) => {
      saveAuthTokens(action.payload);
      state.sessionActive = true;
    },
  },
  extraReducers: (builder) => {
    builder.addMatcher(authApi.endpoints.signIn.matchFulfilled, (state, action) => {
      saveAuthTokens(action.payload);
      state.sessionActive = true;
    });
  },
});

export const { setError, clearError, setRefreshingToken, setUser } = slice.actions;
export default slice.reducer;
