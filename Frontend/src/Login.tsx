import { useState } from "react";
import { useLoginMutation } from "./services/loginApi";
import LoginRequest from "./models/LoginRequest";
import { useAppDispatch, useAppSelector } from "./store/store";
import Slice from "./store/slice";
import LoginResponse from "./models/LoginResponse";
import { mainApi } from "./services/mainApi";

const Login = ():JSX.Element => {
  const [username, setUsername] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [loginFailed, setLoginFailed] = useState<boolean>(false);

  const [login] = useLoginMutation();

  const dispatch = useAppDispatch();
  const slice = useAppSelector(s => s.slice);
  if(slice.loginUser)
    return(
      <div>
        <div>You Are Logged in as {slice.loginUser.username}!</div>
        <button
          onClick={()=>{
            dispatch(Slice.actions.setLoginUser(undefined));
            dispatch(mainApi.util.invalidateTags(["adminContent", "userContent"]));
          }}
        >
          Logout
        </button>

      </div>
    )

  return (
    <div>
      <div>Login</div>
      <div>
        Username:
        <input
          value={username}
          onChange={(e)=>{
            setUsername(e.target.value);
          }}
        />
      </div>
      <div>
        Password :
        <input
          type="password"
          value={password}
          onChange={(e)=>{
            setPassword(e.target.value);
          }}
        />
      </div>
      <div>
        <button
          type="submit"
          onClick={()=>{
            login({
              Username: username,
              Password: password
            } as LoginRequest)
              .unwrap()
              .then((res)=>{
                console.log("res", res);
                setLoginFailed(false);
                dispatch(Slice.actions.setLoginUser(res as (LoginResponse & void)));
              })
              .catch(()=>{
                setLoginFailed(true);
              })
          }}
        >
          Submit
        </button>
        {loginFailed && <div>Login Failed</div>}
      </div>

    </div>
  );
}
export default Login;
