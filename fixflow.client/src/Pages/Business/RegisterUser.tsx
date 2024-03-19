import React, { useState } from 'react';
import Card from '../../Components/Card/Card';

interface UserProps {
  FullName: string;
  CPF: string;
  Email: string;
  PhoneNumber: string;
}

const RegisterUser: React.FC<{ userType: string }> = ({ userType }) => {

  const [userForm, setUserForm] = useState<UserProps>({
    FullName: '',
    CPF: '',
    Email: '',
    PhoneNumber: ''
  });

  return (
    <div>
      Not yet ready
    </div>
  );
};

export default RegisterUser;