'use client'
import { Container, Modal, ModalBody, ModalCloseButton, ModalContent, ModalHeader, ModalOverlay, useDisclosure, useToast } from '@chakra-ui/react'
import React, { ChangeEvent, useLayoutEffect, useState } from 'react'
import { useRouter, useSearchParams } from 'next/navigation'
import StepOne from './StepOne'
import StepTwo from './StepTwo'
import StepThree from './StepThree'
import axios from 'axios'
import { useAccount } from '@/hook/myAccount'
import Finished from './Finished'

interface ICreateCompany {
    typeId?: string,
    specializationId?: string,
    userId: string,
    name?: string,
    description?: string
}
interface IStatus {
    created: boolean,
    isLoading: boolean,
    error?: string,
}
const Init = () => {
    const { user } = useAccount()
    const [form, setForm] = useState<ICreateCompany>({ typeId: undefined, specializationId: undefined, userId: user?.userId ?? "", name: undefined });
    const [crateStatus, setCreateStatus] = useState<IStatus>({ created: false, isLoading: false, error: undefined })
    const step = useSearchParams()?.get("step");
    const router = useRouter()

    function handleSelectType(typeId: string) {
        setForm({
            ...form,
            typeId: typeId
        })
    }
    function handleSelectSpecialization(specializationId: string) {
        setForm({
            ...form,
            specializationId: specializationId
        })
    }
    function handleSpecializationBack() {
        setForm({
            ...form,
            specializationId: undefined
        })
    }
    function handleChange(e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) {
        setForm({
            ...form,
            [e.target.name]: e.target.value
        })
    }
    async function handleCreate() {
        setCreateStatus({
            ...crateStatus,
            isLoading: true,
            error: undefined
        })
        if (user) {
            if (form.typeId && form.specializationId && form.name) {
                setForm({ ...form, userId: user.userId })
                try {
                    console.log(form)
                    let data = await axios.post("/api/company/create", form)
                    if (data.status == 200 || data.status == 201) {
                        setCreateStatus({
                            created: true,
                            isLoading: false,
                            error: undefined
                        })
                        router.push("/create-a-company?step=4")
                    } else {
                        setCreateStatus({
                            created: false,
                            isLoading: false,
                            error: "Se produjo un error al crear negocio"
                        })
                    }
                } catch (e: any) {
                    setCreateStatus({
                        created: false,
                        isLoading: false,
                        error: "Existe un negocio con el nombre " + form.name
                    })
                }
            } else {
                setCreateStatus({
                    created: false,
                    isLoading: false,
                    error: "Faltan datos"
                })
            }
        } else {
            setCreateStatus({
                created: false,
                isLoading: false,
                error: "Se produjo un erro al crar negocio"
            })
        }

    }
    function cleanStatus() {
        setCreateStatus({
            created: false,
            isLoading: false,
            error: undefined
        })
    }

    switch (step) {
        case "1":
            return <Body status={crateStatus} cleanStatus={cleanStatus} title="Selecciona el tipo de negocio" ><StepOne onSelect={(typeId) => handleSelectType(typeId)} /></Body>
        case "2":
            return <Body status={crateStatus} cleanStatus={cleanStatus} title="Selecciona la especialidad" ><StepTwo onBack={() => handleSpecializationBack()} form={form} onSelect={(sepecializationId) => handleSelectSpecialization(sepecializationId)} /></Body>
        case "3":
            return <Body status={crateStatus} cleanStatus={cleanStatus} title="Perfil del negocio" ><StepThree createStatus={crateStatus} onCreate={() => handleCreate()} form={form} onChange={(e) => handleChange(e)} /></Body>
        case "4":
            return <Body status={crateStatus} cleanStatus={cleanStatus} title="Configuracion terminada" ><Finished newCompany={{created:crateStatus.created}} /></Body>
        default:
            return <Body status={crateStatus} cleanStatus={cleanStatus} title="Selecciona el tipo de negocio" ><StepOne onSelect={(typeId) => handleSelectType(typeId)} /></Body>
    }
}

function Body({ children, title, status, cleanStatus }: { children: React.ReactNode, title: string, status: IStatus, cleanStatus: () => void }) {
    const { isOpen, onOpen, onClose } = useDisclosure()
    const toast = useToast()
    const router = useRouter()
    useLayoutEffect(() => {
        onOpen()
    }, [])

    useLayoutEffect(() => {
        if (status.error) {
            toast({
                title: status.error,
                status: "error",
                position: "top",
                isClosable: true,
            })
        }
        () => {
            cleanStatus()
        }
    }, [status])
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
                    {
                        status.created ?
                            ""
                            :
                            <ModalCloseButton />
                    }
                    <ModalBody py="20px">
                        {children}
                    </ModalBody>
                </ModalContent>
            </Modal>
        </Container>
    )
}

export default Init
