import useUser from '@/hook/useUser';
import { useRouter } from 'next/navigation';
import React from 'react'

export const WithAuth = (Component:any) => {
    const Auth = (props:any) => {
        const { session } = useUser();
        const Router = useRouter();
        return (
          session == false ? Router.push("/api/auth/login") :<Component props={...props}/>
        )
      }; 
      return Auth;
}
