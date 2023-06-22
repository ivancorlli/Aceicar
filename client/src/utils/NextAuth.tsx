'use client'
import React from 'react'
import { UserProvider } from '@auth0/nextjs-auth0/client';

type Props = {
  children: React.ReactNode
}

const NextAuth = ({ children }: Props) => {
  return (
    <>
      <UserProvider>
        {children}
      </UserProvider>
    </>
  )
}

export default NextAuth