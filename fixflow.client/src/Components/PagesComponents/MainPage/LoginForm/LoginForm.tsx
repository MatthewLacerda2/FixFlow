import { useState } from "react";
import { FlowLoginRequest, LoginService } from "../../../../FlowApi";

export const LoginForm: React.FC = () => {
  const [loginData, setLoginData] = useState<FlowLoginRequest>({
    userName: "",
    email: "",
    password: "",
  });

  const handleInputChange = (
    e: React.ChangeEvent<HTMLInputElement>,
    field: string
  ) => {
    const { value } = e.target;
    setLoginData((prevData) => ({
      ...prevData,
      [field]: value,
    }));
  };

  const handleLogin = () => {
    let result = LoginService.postApiV1Login(loginData);
  };

  return (
    <div>
      <div className="login-form">
        <input
          type="text"
          placeholder="Enter UserName or Email"
          value={loginData.userName as string}
          onChange={(e) => handleInputChange(e, "userName")}
        />
        <input
          type="password"
          placeholder="Password"
          value={loginData.password}
          onChange={(e) => handleInputChange(e, "password")}
        />
        <br />
        <button onClick={handleLogin}>Login</button>
      </div>
    </div>
  );
};
