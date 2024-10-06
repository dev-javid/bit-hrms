import ChangePasswordForm from '@/app/auth/change-password-form';
import { useSignOutMutation } from '@/lib/rtk/rtk.auth';
import { deleteAauthTokens } from '@/lib/types';
import { User } from 'lucide-react';
import { useNavigate } from 'react-router-dom';
import { Button, DropdownMenu, DropdownMenuTrigger, DropdownMenuContent, DropdownMenuSeparator, DropdownMenuItem, useSimpleModal } from 'xplorer-ui';

const UserMenu = () => {
  const navigateTo = useNavigate();
  const { showModal } = useSimpleModal();
  const [signOut] = useSignOutMutation();

  const onSignoutClick = async () => {
    const res = await signOut(null);
    if (!('error' in res)) {
      deleteAauthTokens();
      navigateTo('/');
    }
  };

  const onMyAccountClick = () => {
    showModal(`Change Password `, <ChangePasswordForm />);
  };

  return (
    <DropdownMenu>
      <DropdownMenuTrigger asChild>
        <Button size="icon" className="overflow-hidden rounded-full">
          <User />
        </Button>
      </DropdownMenuTrigger>
      <DropdownMenuContent align="end">
        <DropdownMenuItem className="cursor-pointer" onClick={onMyAccountClick}>
          Change Password
        </DropdownMenuItem>
        <DropdownMenuItem className="cursor-pointer">Support</DropdownMenuItem>
        <DropdownMenuSeparator />
        <DropdownMenuItem className="cursor-pointer" onClick={onSignoutClick}>
          Sign-out
        </DropdownMenuItem>
      </DropdownMenuContent>
    </DropdownMenu>
  );
};

export default UserMenu;
