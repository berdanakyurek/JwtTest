import { createSlice } from '@reduxjs/toolkit';
import LoginResponse from '../models/LoginResponse';

// 1
interface State {
  loginUser: LoginResponse;
}

// 2
const initialState: State = {
  loginUser: null,
}

// 3
const reducers = {
  setLoginUser: (state: State, action: any) => {
    state.loginUser = action.payload;
  }
}

const Slice = createSlice({
  name: 'slice',
  initialState,
  reducers: reducers,
})

export default Slice;
