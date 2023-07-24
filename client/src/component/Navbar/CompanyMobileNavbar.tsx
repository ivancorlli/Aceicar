'use client'

import { HStack } from '@chakra-ui/react'
import React from 'react'
import CompanyMobileMenu from '../Menu/CompanyMobileMenu'

const CompanyMobileNavbar = () => {
  return (
    <HStack
    display={["block","none"]}
    position="sticky"
    top="0"
    left="0"
    >
        <CompanyMobileMenu/>
    </HStack>
  )
}

export default CompanyMobileNavbar