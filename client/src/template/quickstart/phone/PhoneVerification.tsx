'use client'
import UserAccount from '@/component/Card/UserAccount'
import EnterPin from '@/component/Input/EnterPin'
import { VStack } from '@chakra-ui/react'
import React from 'react'

const PhoneVerification = () => {
    return (
        <VStack spacing={8} w="100%" alignItems="center" justifyContent="center">
            <UserAccount />
            <EnterPin/>
        </VStack>
    )
}

export default PhoneVerification