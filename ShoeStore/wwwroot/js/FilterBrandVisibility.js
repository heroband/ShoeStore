function toggleFilterText(label) {
    const isExpanded = label.getAttribute("aria-expanded") === "true";
    label.textContent = isExpanded ? "Hide" : "Show more";
}