import { configureStore } from '@reduxjs/toolkit'

import { loginApi } from '../services/loginApi';
import { mainApi } from '../services/mainApi';
import Slice from './slice';
import { TypedUseSelectorHook, useDispatch, useSelector } from 'react-redux';

export const store = configureStore({
  reducer: {
    slice: Slice.reducer,
    [mainApi.reducerPath]: mainApi.reducer,
    [loginApi.reducerPath]: loginApi.reducer,
  },

  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware({serializableCheck: false})
      .concat(mainApi.middleware)
      .concat(loginApi.middleware),
})

export default store;
export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
export const useAppDispatch = () => useDispatch<AppDispatch>();
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector;
