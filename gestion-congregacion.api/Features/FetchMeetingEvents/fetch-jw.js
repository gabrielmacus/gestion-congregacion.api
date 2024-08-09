function getSection(element) {
    if (element.classList.contains("du-color--teal-700")) return 1
    if (element.classList.contains("du-color--gold-700")) return 2
    if (element.classList.contains("du-color--maroon-600")) return 3
}
function getDuration(title, section) {
    let textContent = section === undefined ?
        title.textContent :
        title.nextElementSibling.firstElementChild.textContent
    const match = textContent.match(/\(([0-9]+) mins.\)|\(([0-9]) min.\)/)
    if (!match) return
    return parseInt(match[1] ?? match[2])
}
function getTitleText(title, section) {
    let titleText = title.textContent
    if (section === undefined) return titleText
    return titleText.replace(/^([0-9]+\. )/g, "")
}
function getNumber(title) {
    const match = title.textContent.match(/^([0-9]+)\. /)
    if (!match) return
    return parseInt(match[1])
}
function getDescription(title, section) {
    if (section != 2) return
    let description = title.nextElementSibling.firstElementChild.textContent
    description = description.replace(/\(([0-9]+) mins.\)|\(([0-9]) min.\)/, "")
    description = description.replace("\n", "")
    description = description.match(/^.*?[\.!\?](?:\s|$)/g)[0].replace(".","")
    return description.trim()
}

const events = []

const eventTitles = document
    .querySelector(".todayItem")
    .querySelectorAll("h3[data-pid]")

eventTitles.forEach((title, index) => {
    const Section = getSection(title)
    const Duration = getDuration(title, Section)
    const titleText = getTitleText(title, Section)
    const Number = getNumber(title)
    const Description = getDescription(title, Section)

    events.push({
        Section,
        Duration,
        Title: titleText,
        Order: index,
        Description,
        Number
    })
})

events