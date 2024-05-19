import React from "react";
import Card from "../../../Components/Card/Card";
import FlowButton from "../../../Components/FlowButton/FlowButton";

const ResetLinkPage: React.FC = () => {
  //we get the token from useParams()
  //we get the PasswordResetRequest from AccountsService.patchApiV1AccountsResetLink(token);

  //if the PRR comes back null, we write the message we got (since we got a badrequest() anyway), and DONT render the <Card>

  //if comes back valid:
  //we write the email and let the user write the password and confirm password
  //when the person hits the 'Send' button, we send it to AccountsService.patchApiV1AccountsPasswordResetRequest(PR)

  //if we get a 200 result, than we write a simple "Senha resetada, tente fazer login agora" and DONT render the <Card>
  //otherwise, we write the error message we got, underneath the 'Send' button

  function send(): void {
    console.log("Sent");
  }

  return (
    <div style={{ fontSize: "20px" }}>
      <Card title="Reset sua senha">
        <p>
          <b>Email:</b> email
        </p>
        <p>
          <b>New Password:</b> password
        </p>
        <p>
          <b>Confirm password:</b> confirmPassword
        </p>
        <FlowButton text="Send" onClick={send} />
      </Card>
    </div>
  );
};

export default ResetLinkPage;
