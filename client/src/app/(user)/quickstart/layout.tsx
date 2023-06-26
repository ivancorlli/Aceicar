import QuickStartLayout from '@/template/quickstart/QuickStartLayout'
import React from 'react'

const layout = ({children}:{children:React.ReactNode}) => {
  return (
    <QuickStartLayout>
      {children}
    </QuickStartLayout>
  )
}

export default layout