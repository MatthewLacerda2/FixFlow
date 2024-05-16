import React from "react";

const NotFoundPage: React.FC = () => {
  return (
    <div>
      <h1 style={{ fontSize: "74px", marginBottom: "22px" }}>
        ERRO 404: NOT FOUND
      </h1>
      <p style={{ textAlign: "center", fontSize: "17px" }}>
        This is not the content you've been looking for
      </p>
    </div>
  );
};

export default NotFoundPage;
