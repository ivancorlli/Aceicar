'use client'
import { Container, Modal, ModalBody, ModalContent, ModalHeader, ModalOverlay, useDisclosure, useToast } from '@chakra-ui/react'
import React, { useLayoutEffect} from 'react'
import { useRouter, useSearchParams } from 'next/navigation'
import StepFour from './StepFour'
import StepFive from './StepFive'

const Init = () => {
    const step = useSearchParams()?.get("step");
    switch (step) {
        case "1":
            return <Body title='Informacion de contacto' ><StepFour/></Body>
        case "2":
            return <Body title='Ubicacion'><StepFive/></Body>
        default:
            return <Body title='Informacion de contacto' ><StepFour/></Body>
    }

}

function Body({title,children}:{title:string,children:React.ReactNode}) {
    const { isOpen, onOpen, onClose } = useDisclosure()
    const toast = useToast()
    const router = useRouter()
    useLayoutEffect(() => {
        onOpen()
    }, [])


    function handleClose() {
        onClose()
        return router.push("/")
    }

    return (
        <Container>
            <Modal isOpen={isOpen} onClose={handleClose} closeOnOverlayClick={false} >
                <ModalOverlay />
                <ModalContent>
                    <ModalHeader fontWeight="bold">{title}</ModalHeader>
                    <ModalBody py="20px">
                        {children}
                    </ModalBody>
                </ModalContent>
            </Modal>
        </Container>
    )
}

export default Init