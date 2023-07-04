import { UserContext } from '@/context/UserContext'
import React, { useContext } from 'react'

const useUser = () => {
  const {user,isLoading} = useContext(UserContext)
  return {
    user,
    isLoading,
  }
}

export default useUser