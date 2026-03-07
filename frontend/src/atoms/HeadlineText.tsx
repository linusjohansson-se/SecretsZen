interface Props {
  text: string
}

export default function HeadlineText({ text }: Props) {
  return (
    <h1>{text}</h1>
  )
}
