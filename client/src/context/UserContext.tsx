'use client'
import User from '@/lib/entity/User';
import { UserStatus } from '@/lib/enum/UserStatus';
import IUser from '@/lib/interface/IUser';
import { useUser } from '@auth0/nextjs-auth0/client';
import React, { createContext,useEffect,useState } from 'react'


export const UserContext = createContext<{user:IUser | null, isLoading:boolean}>({user:null,isLoading:false});

const UserProvider = ({ children}: { children: React.ReactNode}) => {
    const { user: session, isLoading } = useUser();
    const [user, setUser] = useState<IUser | null>(null)

    useEffect(()=>{
        if(session?.sub != null)
        {
            const user:User = new User(session.sub!,session.email!,UserStatus.Active,session.updated_at!,session.updated_at!);
            if (session.picture != null) user.ProfileImage = session.picture
            setUser(user)
        }else {
            setUser(null)
        }
    },[session])
    const value = {
        user,
        isLoading,
    }
    return (
        <UserContext.Provider value={{...value}}>
            {children}
        </UserContext.Provider>
    );
};

export default UserProvider