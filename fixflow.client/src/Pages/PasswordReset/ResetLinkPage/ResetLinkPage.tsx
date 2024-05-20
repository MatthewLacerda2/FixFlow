import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import Card from "../../../Components/Card/Card";
import FlowButton from "../../../Components/FlowButton/FlowButton";
import { AccountsService, PasswordResetRequest } from "../../../FlowApi";

const ResetLinkPage: React.FC = () => {
  const { token } = useParams<{ token: string }>();
  const [email, setEmail] = useState<string | null>(null);
  const [password, setPassword] = useState<string>("");
  const [confirmPassword, setConfirmPassword] = useState<string>("");
  const [errorMessage, setErrorMessage] = useState<string | null>(null);
  const [successMessage, setSuccessMessage] = useState<string | null>(null);

  if (token === undefined) {
    return <div>Null Token</div>;
  }

  useEffect(() => {
    if (token) {
      AccountsService.patchApiV1AccountsResetLink(token)
        .then((response) => {
          setEmail(response.email);
        })
        .catch(() => {
          setErrorMessage("Invalid or expired reset link.");
        });
    }
  }, [token]);

  const handlePasswordReset = () => {
    if (password !== confirmPassword) {
      setErrorMessage("Passwords do not match.");
      return;
    }

    const passwordResetRequest: PasswordResetRequest = {
      email,
      token,
      password,
      confirmPassword,
    };

    AccountsService.patchApiV1AccountsResetRequest(passwordResetRequest)
      .then(() => {
        setSuccessMessage(
          "Password reset successfully. Please try logging in now."
        );
        setEmail(null); // Stop rendering the Card
      })
      .catch(() => {
        setErrorMessage("Failed to reset password. Please try again.");
      });
  };

  if (errorMessage) {
    return <div style={{ fontSize: "20px" }}>{errorMessage}</div>;
  }

  if (successMessage) {
    return <div style={{ fontSize: "20px" }}>{successMessage}</div>;
  }

  return (
    <div style={{ fontSize: "20px" }}>
      {email && (
        <Card title="Reset sua senha">
          <p>
            <b>Email:</b> {email}
          </p>
          <p>
            <b>New Password:</b>
            <input
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
          </p>
          <p>
            <b>Confirm password:</b>
            <input
              type="password"
              value={confirmPassword}
              onChange={(e) => setConfirmPassword(e.target.value)}
            />
          </p>
          <FlowButton text="Send" onClick={handlePasswordReset} />
          {errorMessage && <p style={{ color: "red" }}>{errorMessage}</p>}
        </Card>
      )}
    </div>
  );
};

export default ResetLinkPage;
