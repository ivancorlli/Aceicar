'use client'
import { Center, Flex } from '@chakra-ui/react'
import React from 'react'

const QuickStartLayout = ({children}:{children:React.ReactNode}) => {
  return (
    <>
    <Center h="100%">
        {children}
    </Center>
    </>
  )
}

export default QuickStartLayout